import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { environment } from '../../../../../environments/environment';
import { BloodType } from '../../../../config/enums/blood-types.enum';
import { BloodTypeModel, BloodTypes } from '../../../../config/models/blood-types.model';
import { LabTypes, MedicalStaffTypeModel, MedicalTypes, NurseTypes } from '../../../../config/models/medical-staff-types.model';
import { LoaderService } from '../../../../config/services/loader.service';
import { ToastService } from '../../../../config/services/toast.service';
import { AuthService } from '../../../auth/services/auth.service';
import { AdvImageModel } from '../../models/adv-image.model';
import { AdvImageService } from '../../services/adv-image.service';

@Component({
  selector: 'app-home-display',
  templateUrl: './home-display.component.html',
  styleUrls: ['./home-display.component.css']
})
export class HomeDisplayComponent implements OnInit {

  responsiveOptions = [
    {
      breakpoint: '1024px',
      numVisible: 3,
      numScroll: 3
    },
    {
      breakpoint: '768px',
      numVisible: 2,
      numScroll: 2
    },
    {
      breakpoint: '560px',
      numVisible: 1,
      numScroll: 1
    }
  ];

  isLoaded = false;
  images: AdvImageModel[] = [];

  loggedIn = false;
  userName: string = "";
  bloodTypes: BloodTypeModel[] = BloodTypes();
  labTypes: MedicalStaffTypeModel[] = LabTypes();
  nurseTypes: MedicalStaffTypeModel[] = NurseTypes();
  medicalTypes: MedicalStaffTypeModel[] = MedicalTypes();

  constructor(
    private authService: AuthService,
    private router: Router,
    private toastService: ToastService,
    private advImageService: AdvImageService,
    private loaderService: LoaderService
  ) { }

  async ngOnInit() {
    this.loaderService.showLoader();

    this.loggedIn = await this.authService.isLoggedIn();
    this.images = await this.advImageService.getImages();
    this.userName = await this.authService.userName();

    this.isLoaded = true;
    this.loaderService.hideLoader();
  }

  getImage(imagePath: string){
    return `${environment.apiURL}${environment.imageSegment}${imagePath}`;
  }

  onBloodTypeClick(type: BloodType){
    if(this.loggedIn){
      this.router.navigateByUrl(`/donors?type=${type}`);
    }else{
      this.toastService.showWarning("عفوا , يجب عليك تسجيل الدخول اولا");
    }
  }

}
