import { Routes } from '@angular/router';
import { CdbComponent } from './cdb/cdb.component';

export const routes: Routes = [
  { path: '', redirectTo: 'cdb-calculator', pathMatch: 'full' },
  { path: 'cdb-calculator', component: CdbComponent }
];