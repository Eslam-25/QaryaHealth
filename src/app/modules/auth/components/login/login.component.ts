import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { LoaderService } from '../../../../config/services/loader.service';
import { ToastService } from '../../../../config/services/toast.service';
import { LoginModel } from '../../models/login.model';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  submitted = false;
  loginModel: LoginModel = {} as LoginModel;
  loginForm: FormGroup = new FormGroup({});

  constructor(
    private fb: FormBuilder,
    private toastService: ToastService,
    private loaderService: LoaderService,
    private authService: AuthService,
    private router: Router
    ) { }

  ngOnInit(): void {
    this.prepareFormGroup();
  }

  prepareFormGroup(){
    this.loginForm = this.fb.group({
      phoneNumber: ['', Validators.required],
      password: ['', Validators.required],
    });
  }

  get loginFormControls() {
    return this.loginForm.controls;
  }

  mapModelFromUI() {
    this.loginModel.phoneNumber = this.loginForm.get("phoneNumber")?.value;
    this.loginModel.password = this.loginForm.get("password")?.value;
  }

  async onSave() {
    this.submitted = true;
    if (this.loginForm.valid) {

      this.mapModelFromUI();
      this.loaderService.showLoader();
      let isLoggedIn = await this.authService.login(this.loginModel);
      
      if(isLoggedIn){
        this.toastService.show("تم الدخول بنجاح");
        
        this.router.navigateByUrl("/home").then(() => location.reload());
      }else{
        this.toastService.show("تأكد من رقم الهاتف او الرقم السري");
        this.loaderService.hideLoader();
      }
      
    }
  }

}
