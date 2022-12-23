import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { StaffDetailComponent } from './components/staff-detail/staff-detail.component';
import { StaffDisplayComponent } from './components/staff-display/staff-display.component';
import { StaffFormComponent } from './components/staff-form/staff-form.component';
import { StaffServicesComponent } from './components/staff-services/staff-services.component';

const routes: Routes = [
  {
    path: '',
    component: StaffDisplayComponent
  },
  {
    path: 'form',
    component: StaffFormComponent
  },
  {
    path: 'details/:id',
    component: StaffDetailComponent
  },
  {
    path: 'services',
    component: StaffServicesComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MedicalStaffRoutingModule { }
