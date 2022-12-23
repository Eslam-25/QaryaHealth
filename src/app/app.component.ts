import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { Router } from '@angular/router';
import { AlertController, Platform } from '@ionic/angular';
import { StatusBar } from '@capacitor/status-bar';
import { SplashScreen } from '@capacitor/splash-screen';
import { LoaderService } from './config/services/loader.service';
import { AuthService } from './modules/auth/services/auth.service';
import { Role } from './config/enums/role.enum';
import { environment } from '../environments/environment';
import { DonorService } from './modules/donors/services/donor.service';
import { ToastService } from './config/services/toast.service';
import { MedicalStaffService } from './modules/medical-staff/services/medical-staff.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class AppComponent implements OnInit {
  appPages = [];

  showLoader = false;
  loggedIn = false;
  isReady = false;
  roleIsDonar = false;
  roleIsMedicalStaff = false;
  roleIsEmployee = false;
  roleIsAdmin = false;

  title = environment.appName;

  constructor(
    private platform: Platform,
    private router: Router,
    private alertController: AlertController,
    private loaderService: LoaderService,
    private authService: AuthService,
    private donorService: DonorService,
    private staffService: MedicalStaffService,
    private toastService: ToastService
  ) {
    this.initializeApp();
  }

  async ngOnInit() {
    await this.prepareRoles();
    this.loaderService.loader.subscribe(value => this.showLoader = value);
  }

  async prepareRoles() {
    this.loggedIn = await this.authService.isLoggedIn();
    let userRole = await this.authService.userRole();

    this.roleIsDonar = userRole == Role.Donar;
    this.roleIsMedicalStaff = userRole == Role.MedicalStaff;
    this.roleIsEmployee = userRole == Role.Employee;
    this.roleIsAdmin = userRole == Role.Admin;

    this.loaderService.showLoader();

    if (this.roleIsDonar)
      await this.prepareDonarCheck();

    if (this.roleIsMedicalStaff)
      await this.prepareStaffCheck();

    this.loaderService.hideLoader();
  }

  async prepareDonarCheck() {
      let userId = await this.authService.userId();
      let donar = await this.donorService.getDonar(userId);
      this.isReady = donar.readyToDonor;
  }

  async prepareStaffCheck() {
    let userId = await this.authService.userId();
    let staff = await this.staffService.getStaff(userId);
    this.isReady = staff.readyToWork;
  }

  initializeApp() {
    this.platform.ready().then(() => {
      if (this.platform.is('hybrid')) {
        StatusBar.hide();
        SplashScreen.hide();
      }
    });
  }

  async logout() {
    const alert = await this.alertController.create({
      header: 'خروج',
      message: 'هل تريد تسجيل الخروج ؟',
      buttons: [
        {
          text: 'نعم',
          role: 'confirm',
          handler: () => {
            this.authService.logout();

            this.router.navigateByUrl("/home").then(() => location.reload());
          },
        },
        {
          text: 'لا',
          role: 'cancel',
        }
      ],
    });

    await alert.present();
  }

  async toggleDonar() {
    this.loaderService.showLoader();
    let userId = await this.authService.userId();
    let isUpdated = await this.donorService.toggleDonoration(userId);
    if (isUpdated) {
      await this.prepareDonarCheck();
      this.toastService.show("تم التعديل بنجاح");
    } else {
      this.toastService.showErrorMsg();
    }

    this.loaderService.hideLoader();
  }

  async toggleStaffReadyToWork() {
    this.loaderService.showLoader();
    let userId = await this.authService.userId();
    let isUpdated = await this.staffService.ToggleWorking(userId);
    if (isUpdated) {
      await this.prepareStaffCheck();
      this.toastService.show("تم التعديل بنجاح");
    } else {
      this.toastService.showErrorMsg();
    }

    this.loaderService.hideLoader();
  }
}
