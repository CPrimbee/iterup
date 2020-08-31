import { Injectable } from '@angular/core';
import { Observable, of, throwError } from 'rxjs';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { catchError, tap, map } from 'rxjs/operators';
import { Etapa } from 'src/model/etapa';
const httpOptions = {
  headers: new HttpHeaders({'Content-Type': 'application/json'})
};
const apiUrl = 'https://localhost:5001/desafio/workflow';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  constructor(private http: HttpClient) { }

  getProdutos(): Observable<Etapa[]> {
    return this.http.get<Etapa[]>(apiUrl)
      .pipe(
        tap(produtos => console.log('leu os produtos')),
        catchError(this.handleError('getProdutos', []))
      );
  }

  getProduto(id: number): Observable<Etapa> {
    const url = `${apiUrl}/${id}`;
    return this.http.get<Etapa>(url).pipe(
      tap(_ => console.log(`leu o produto id=${id}`)),
      catchError(this.handleError<Etapa>(`getProduto id=${id}`))
    );
  }

  addProduto(produto): Observable<Etapa> {
    return this.http.post<Produto>(apiUrl, produto, httpOptions).pipe(
      // tslint:disable-next-line:no-shadowed-variable
      tap((produto: Produto) => console.log(`adicionou o produto com w/ id=${produto._id}`)),
      catchError(this.handleError<Produto>('addProduto'))
    );
  }

  updateProduto(id, produto): Observable<any> {
    const url = `${apiUrl}/${id}`;
    return this.http.put(url, produto, httpOptions).pipe(
      tap(_ => console.log(`atualiza o produco com id=${id}`)),
      catchError(this.handleError<any>('updateProduto'))
    );
  }

  deleteProduto(id): Observable<Etapa> {
    const url = `${apiUrl}/delete/${id}`;

    return this.http.delete<Etapa>(url, httpOptions).pipe(
      tap(_ => console.log(`remove o produto com id=${id}`)),
      catchError(this.handleError<Etapa>('deleteProduto'))
    );
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {

      console.error(error);

      return of(result as T);
    };
  }
}
