import { Component, inject, OnInit, signal, computed } from '@angular/core';
import { AuthorsService, BookDetailDto, BooksService, BookSummaryDto, DiagnosticsService, LoansService } from './swagger';
import { rxResource } from '@angular/core/rxjs-interop';
import { FormsModule } from '@angular/forms';
import { JsonPipe } from '@angular/common';
import { EMPTY } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  imports: [FormsModule, JsonPipe],
  selector: 'app-root',
  styleUrl: './app.scss',
  templateUrl: './app.html',
})
export class App implements OnInit{
  ngOnInit(): void {
   
  }
  protected readonly title = signal('LibraryExplorer');
  private booksService = inject(BooksService);
  private errorService = inject(DiagnosticsService);
  private authorService = inject(AuthorsService);
  private loansService = inject(LoansService);
  protected books = computed(() => this.booksRef.value());
  protected filterText = signal<string>("");
  protected selectedAuthorId = signal<number | undefined>(undefined);

  protected booksStatus = computed(() => this.booksRef.status());

  protected selectedBookId = signal<number | undefined>(undefined);
  protected bookDetail = computed(() => this.bookDetailRef.value());
  
  private bookDetailRef = rxResource({
    params: this.selectedBookId,
    stream: ({ params }) => this.booksService.booksIdGet(params),
  });

  protected bookError = computed(() => {
    const error = this.bookDetailRef.error();
    if(!(error instanceof HttpErrorResponse)) return error;
    let errorDetail = '';
    if(error.error){
      errorDetail = typeof error.error === 'string'
      ? error.error
      : JSON.stringify(error.error);
    }
    return `HTTP ${error.status}: ${errorDetail.substring(12,errorDetail.length-2)}`;
  });

  private booksRef = rxResource({
    params: () => ({
    filter: this.filterText() || undefined,
    authorId: this.selectedAuthorId() ?? undefined
  }),
    stream: ({ params }) => {
    
    return this.booksService.booksGet(params.filter, params.authorId);
  }
});

  protected loans = computed(() => this.loansRef.value());
  private loansRef = rxResource({
    params: this.selectedBookId,
    stream: ({params: id}) => this.loansService.loansGet(id)
  });



  protected authors = computed(() => this.authorsRef.value());
  private authorsRef = rxResource({
    params:signal(0),
    stream: _ => this.authorService.authorsGet()
  });
  
  resetFilter(){
    this.filterText.set("");
    this.selectedAuthorId.set(undefined);
  }

  reloadCatalogue(){
    this.booksRef.reload();
  }

  protected triggerError = signal(0);
  private errorRef = rxResource({
  params: () => this.triggerError(),
  stream: ({ params }) => {
    if (!params) return EMPTY;

    return this.errorService.diagnosticsFailGet();
  }
});
protected errorMessage = computed(() => {
  const error = this.errorRef.error();

  if (!(error instanceof HttpErrorResponse)) return '';

  return `HTTP failure response for ${error.url}: ${error.status}. The diagnostics endpoint deliberately returns HTTP 500.`;
});
}
