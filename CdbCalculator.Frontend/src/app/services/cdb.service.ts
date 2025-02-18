// cdb.service.ts
import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

interface CdbResult {
  grossYield: number;
  netYield: number;
}

@Injectable({
  providedIn: 'root'
})
export class CdbService {
  private apiUrl = 'http://localhost:5201/api/v1/cdb/calculate';

  constructor(private http: HttpClient) { }

  calculateCdb(initialValue: number, months: number): Observable<CdbResult> {
    return this.http.get<CdbResult>(`${this.apiUrl}?initialValue=${initialValue}&months=${months}`)
      .pipe(
        catchError(this.handleError)
      );
  }

  private handleError(error: HttpErrorResponse) {
    return throwError(() => new Error('Erro ao conectar com a API'));
  }
}