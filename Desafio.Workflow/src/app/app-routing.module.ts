import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { EtapasComponent } from './etapas/etapas.component';

const routes: Routes = [
  {
    path: 'etapas',
    component: EtapasComponent,
    data: {title: 'Workflow Etapas'}
  },
  {
    path: '',
    redirectTo: '/etapas',
    pathMatch: 'full'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
