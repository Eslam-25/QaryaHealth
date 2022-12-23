import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DonarsDisplayComponent } from './components/donars-display/donars-display.component';
import { DonorDetailComponent } from './components/donor-detail/donor-detail.component';

const routes: Routes = [
  {
    path: '',
    component: DonarsDisplayComponent
  },
  {
    path: 'detail',
    component: DonorDetailComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DonorsRoutingModule { }
