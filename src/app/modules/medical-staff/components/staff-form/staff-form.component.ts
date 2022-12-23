import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { environment } from '../../../../../environments/environment';
import { Role } from '../../../../config/enums/role.enum';
import { getMedicalStaffByType, MedicalStaffTypeModel, MedicalTypes } from '../../../../config/models/medical-staff-types.model';
import { LoaderService } from '../../../../config/services/loader.service';
import { ToastService } from '../../../../config/services/toast.service';
import { AuthService } from '../../../auth/services/auth.service';
import { MedicalStaffModel } from '../../models/medical-staff.model';
import { MedicalStaffService } from '../../services/medical-staff.service';

@Component({
  selector: 'app-staff-form',
  templateUrl: './staff-form.component.html',
  styleUrls: ['./staff-form.component.css']
})
export class StaffFormComponent implements OnInit {

  title: string = "";
  operationAdd = true;
  submitted = false;
  staffForm: FormGroup;
  isLoaded = false;

  medicalStaffType: MedicalStaffTypeModel;
  medicalStaff: MedicalStaffModel = {} as MedicalStaffModel;

  constructor(
    private fb: FormBuilder,
    private activatedRoute: ActivatedRoute,
    private loaderService: LoaderService,
    private toastService: ToastService,
    private authService: AuthService,
    private staffService: MedicalStaffService
  ) { }

  async ngOnInit() {
    this.loaderService.showLoader();
    this.prepareFormGroup();

    this.activatedRoute.queryParams.subscribe(async params => {
      this.medicalStaffType = getMedicalStaffByType(params.type);
      this.title = this.medicalStaffType?.name;
      this.operationAdd = params.isAdd;
     
      if(!this.operationAdd){
        await this.getStaffData();
      }else{
        this.loaderService.hideLoader();
      }

      this.isLoaded = true;
    });
  }

  async getStaffData() {
    this.loaderService.showLoader();

    let userId = await this.authService.userId();
    this.medicalStaff = await this.staffService.getStaff(userId);

    if (this.medicalStaff?.id){
      this.medicalStaffType = getMedicalStaffByType(this.medicalStaff.type);
      this.title = this.medicalStaffType.name;
      this.prepareFormGroup();
    }

    this.loaderService.hideLoader();
  }

  prepareFormGroup() {
    this.staffForm = this.fb.group({
      name: [this.medicalStaff.name, Validators.required],
      address: [this.medicalStaff.address, Validators.required],
      phoneNumber: [this.medicalStaff.phoneNumber, Validators.required],
      email: [this.medicalStaff.email, Validators.required],
      password: [this.medicalStaff.password, Validators.required],
      bio: [this.medicalStaff.bio],
      workingDescription: [this.medicalStaff.workingDescription?.split(",")],
    });
  }

  get staffFormControls() {
    return this.staffForm.controls;
  }

  mapModelFromUI() {
    this.medicalStaff.name = this.staffForm.get("name")?.value;
    this.medicalStaff.address = this.staffForm.get("address")?.value;
    this.medicalStaff.bio = this.staffForm.get("bio")?.value;
    this.medicalStaff.phoneNumber = this.staffForm.get("phoneNumber")?.value;
    this.medicalStaff.email = this.staffForm.get("email")?.value;
    this.medicalStaff.password = this.staffForm.get("password")?.value;
    this.medicalStaff.workingDescription = this.staffForm.get("workingDescription")?.value?.toString();
    this.medicalStaff.role = Role.MedicalStaff;
    this.medicalStaff.isActive = true;
    this.medicalStaff.type = this.medicalStaffType.type;
  }

  getImagePath(){
    if(this.medicalStaff?.imagePath?.length)
      return `${environment.apiURL}${environment.imageSegment}${this.medicalStaff.imagePath}`;
      
    return "../../../../../assets/img/male.png";
  }

  async onUploadImage(event: any) {
    const file: File = event.target.files[0];
    const formData = new FormData();
    formData.append('file', file, file.name);

    this.loaderService.showLoader();
    let uploadedImagePath = await this.staffService.uploadImage(formData);
    if(uploadedImagePath){
      
      this.medicalStaff.imagePath = uploadedImagePath?.fileName;
      await this.onUpdate();

    }else{
      this.toastService.showErrorMsg();
    }

    this.loaderService.hideLoader();
  }

  async onSave() {

    this.submitted = true;
    if (this.staffForm.valid) {
      this.loaderService.showLoader();

      this.mapModelFromUI();
      if (this.medicalStaff.id > 0) {
        await this.onUpdate();
      } else {
        await this.onAdd();
      }

      this.loaderService.hideLoader();
    }
  }

  async onAdd() {
    this.medicalStaff.services = [];
    let isAdded = await this.staffService.create(this.medicalStaff);
    if (isAdded) {
      this.toastService.show("تمت الاضافة بنجاح");
      this.staffForm.reset();
      this.staffForm.markAsPristine();
      this.staffForm.markAsUntouched();
      this.staffForm.updateValueAndValidity();
    } else {
      this.toastService.showErrorMsg();
    }
  }

  async onUpdate() {
    let isUpdated = await this.staffService.update(this.medicalStaff);
    if (isUpdated) {
      await this.getStaffData();
      this.toastService.show("تم التعديل بنجاح");
    } else {
      this.toastService.showErrorMsg();
    }
  }
}
