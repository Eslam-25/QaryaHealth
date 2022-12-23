import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { environment } from '../../../../../environments/environment';
import { ToastService } from '../../../../config/services/toast.service';
import { AuthService } from '../../../auth/services/auth.service';
import { MedicalStaffModel } from '../../models/medical-staff.model';

@Component({
  selector: 'staff-card',
  templateUrl: './staff-card.component.html',
  styleUrls: ['./staff-card.component.css']
})
export class StaffCardComponent implements OnInit {

  @Input() staff: MedicalStaffModel = {} as MedicalStaffModel;
  constructor(
    private authService: AuthService,
    private router: Router,
    private toastService: ToastService
    ) { }

  ngOnInit() {

  }

  getImagePath(medicalStaff: MedicalStaffModel){
    if(medicalStaff?.imagePath?.length)
      return `${environment.apiURL}${environment.imageSegment}${medicalStaff.imagePath}`;
      
    return "../../../../../assets/img/male.png";
  }

  async onOpenDetail(staffId: number){
    let loggedIn = await this.authService.isLoggedIn();
    if(loggedIn){
      this.router.navigateByUrl(`medical-saff/details/${staffId}`);
    }else{
      this.toastService.showWarning("عفوا , يجب عليك تسجيل الدخول اولا");
    }
  }
}
