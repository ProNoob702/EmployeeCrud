import { Routes } from '@angular/router';
import { FetchDataComponent } from '../components/fetch-data/fetch-data.component';
import { HomeComponent } from '../components/home/home.component';

export const routes: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: 'fetch-data', component: FetchDataComponent },
];
