import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdvImageDisplayComponent } from './components/adv-image-display/adv-image-display.component';
import { HomeDisplayComponent } from './components/home-display/home-display.component';

const routes: Routes = [
  {
    path: '',
    component: HomeDisplayComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class HomeRoutingModule { }
