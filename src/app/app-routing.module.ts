import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdvImageDisplayComponent } from './modules/home/components/adv-image-display/adv-image-display.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: '/home',
    pathMatch: 'full'
  },
  {
    path: 'adv-images',
    component: AdvImageDisplayComponent
  },
  {
    path: 'auth',
    loadChildren: () => import('./modules/auth/auth.module').then(m => m.AuthModule)
  },
  {
    path: 'home',
    loadChildren: () => import('./modules/home/home.module').then(m => m.HomeModule)
  },
  {
    path: 'donors',
    loadChildren: () => import('./modules/donors/donors.module').then(m => m.DonorsModule)
  },
  {
    path: 'medical-saff',
    loadChildren: () => import('./modules/medical-staff/medical-staff.module').then(m => m.MedicalStaffModule)
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
