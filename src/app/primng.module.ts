import { NgModule } from "@angular/core";
import {InputTextModule} from 'primeng/inputtext';
import {InputMaskModule} from 'primeng/inputmask';
import {CalendarModule} from 'primeng/calendar';
import {RatingModule} from 'primeng/rating';
import {FileUploadModule} from 'primeng/fileupload';
import {HttpClientModule} from '@angular/common/http';
import {PasswordModule} from 'primeng/password';
import {InputNumberModule} from 'primeng/inputnumber';
import {ProgressSpinnerModule} from 'primeng/progressspinner';
import {CarouselModule} from 'primeng/carousel';
import {InputTextareaModule} from 'primeng/inputtextarea';
import {ChipsModule} from 'primeng/chips';
import {ToastModule} from 'primeng/toast';
import { MessageService } from "primeng/api";

@NgModule({
    imports: [
        InputTextModule,
        InputMaskModule,
        CalendarModule,
        RatingModule,
        FileUploadModule,
        HttpClientModule,
        PasswordModule,
        InputNumberModule,
        ProgressSpinnerModule,
        CarouselModule,
        InputTextareaModule,
        ChipsModule,
        ToastModule
    ],
    exports: [
        InputTextModule,
        InputMaskModule,
        CalendarModule,
        RatingModule,
        FileUploadModule,
        HttpClientModule,
        PasswordModule,
        InputNumberModule,
        ProgressSpinnerModule,
        CarouselModule,
        InputTextareaModule,
        ChipsModule,
        ToastModule
    ],
    providers: [
        MessageService
    ]
})
export class PrimeNgAppModule { }