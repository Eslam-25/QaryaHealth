import { Injectable } from "@angular/core";
import { environment } from "../../../../environments/environment";
import { HttpService } from "../../../config/services/http.service";
import { ForgetPasswordModel } from "../models/forget-password.model";
import { LoginModel } from "../models/login.model";
import { RegisterDonorModel } from "../models/register-donor.model";
import { JwtHelperService } from '@auth0/angular-jwt';
import { Role } from "../../../config/enums/role.enum";
import { Storage } from '@ionic/storage';

@Injectable()
export class AuthService {
    segment: string = "auth/";
    helper = new JwtHelperService();

    constructor(
        private httpService: HttpService,
        private storage: Storage) { }

    sendEmail(name: string, email: string, birthDate: Date) {
        return this.httpService.post(`${this.segment}send-email`, {
            "to": email,
            "subject": `${environment.appName} - كود تأكيد`,
            "body": this.prepareEmailBody(name, birthDate)
        }).toPromise();
    }

    async getUserByEmail(email: string) {
        return await this.httpService.get<RegisterDonorModel>(`user/getByEmail?email=${email}`).toPromise<RegisterDonorModel>();
    }

    async updateUser(userModel: RegisterDonorModel) {
        return await this.httpService.put(`user`, userModel).toPromise<boolean>();
    }

    async register(userModel: RegisterDonorModel) {
        var response = await this.httpService.post(`${this.segment}registerDonor`, userModel).toPromise();
        if(response["isAuthenticated"]){
            await this.setLocalStorageValues(response["token"]);
        }

        return response["isAuthenticated"];
    }

    async login(loginModel: LoginModel) {
        let response = await this.httpService.postWithJsonReponse(`${this.segment}login`, loginModel).toPromise();
        if(response["isAuthenticated"]){
            this.storage.clear();
            await this.setLocalStorageValues(response["token"]);
        }

        return response["isAuthenticated"];
    }

    async setLocalStorageValues(token: string){
        const decodedToken = this.helper.decodeToken(token);
        await this.storage.set('token', token);
        // localStorage.set('refreshToken', loginResponse.refreshToken);
        await this.storage.set('userId', +decodedToken.id);
        await this.storage.set('name', decodedToken.name);
        await this.storage.set('phone', decodedToken.phone);
        await this.storage.set('role', decodedToken.role);
    }

    logout(){
        this.storage.clear();
    }

    isLoggedIn = async (): Promise<boolean> => {
        return await this.storage.get("userId") > 0;
    }
    
    userId = async (): Promise<number> => {
        return await this.storage.get("userId") ?? 0;
    }

    userName = async (): Promise<string> => {
        return await this.storage.get("name") ?? "";
    }

    userPhone = async (): Promise<string> => {
        return await this.storage.get("phone") ?? "";
    }
    
    userRole = async (): Promise<Role> => {
        let role = await this.storage.get("role") ?? "";
        return Role[`${role}`];
    }

    forgetPassword(forgetModel: ForgetPasswordModel) {

    }

    prepareEmailBody(name:string, birthDate: Date) {
        return `
            <h3>مرحبا ${name}</h3>
            <h3><b>كود التأكيد : ${this.verificationCode(birthDate)}</b></h3>
            <h4>شكرا لاستخدامك ${environment.appName}</h4>
        `;
    }

    verificationCode(birthDate: Date) {
        let currentDate = new Date();
        return `${birthDate.getDate()}${currentDate.getDate()}`;
    }

    verifyCode(enteredCode: string, birthDate: Date) {
        return enteredCode == this.verificationCode(birthDate);
    }
}