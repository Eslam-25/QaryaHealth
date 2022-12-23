import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Role } from '../../../../config/enums/role.enum';
import { getMedicalStaffByType, MedicalStaffTypeModel } from '../../../../config/models/medical-staff-types.model';
import { LoaderService } from '../../../../config/services/loader.service';
import { AuthService } from '../../../auth/services/auth.service';
import { MedicalStaffModel } from '../../models/medical-staff.model';
import { MedicalStaffService } from '../../services/medical-staff.service';

@Component({
  selector: 'app-staff-display',
  templateUrl: './staff-display.component.html',
  styleUrls: ['./staff-display.component.css']
})
export class StaffDisplayComponent implements OnInit {

  isLoaded = false;
  canAddStaff = false;
  medicalStaffType: MedicalStaffTypeModel = {} as MedicalStaffTypeModel;
  medicalStuffes: MedicalStaffModel[] = [];

  constructor(
    private activatedRoute: ActivatedRoute,
    private authService: AuthService,
    private staffService: MedicalStaffService,
    private loaderService: LoaderService
  ) { }

  async ngOnInit() {
    this.loaderService.showLoader();
    
    await this.prepareRole();

    this.activatedRoute.queryParams.subscribe(async params => {
      this.medicalStaffType = getMedicalStaffByType(params.type);
      await this.prepareStaffByType();
      this.isLoaded = true;
    });
  }
  
  async prepareRole(){
    let role = await this.authService.userRole();
    this.canAddStaff = role == Role.Admin || role == Role.Employee;
  }

  async prepareStaffByType(){
    this.loaderService.showLoader();
    this.medicalStuffes = await this.staffService.getStaffes(this.medicalStaffType.type);
    this.loaderService.hideLoader();
  }

}
