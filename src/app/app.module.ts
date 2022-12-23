import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { InAppBrowser } from '@ionic-native/in-app-browser/ngx';
import { IonicModule } from '@ionic/angular';
import { IonicStorageModule } from '@ionic/storage';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ServiceWorkerModule } from '@angular/service-worker';
import { environment } from '../environments/environment';
import { FormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpService } from './config/services/http.service';
import { ToastService } from './config/services/toast.service';
import { PrimeNgAppModule } from './primng.module';
import { LoaderService } from './config/services/loader.service';
import { AuthModule } from './modules/auth/auth.module';
import { AdvImageService } from './modules/home/services/adv-image.service';

@NgModule({
  imports: [
    BrowserAnimationsModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    PrimeNgAppModule,
    AuthModule,
    IonicModule.forRoot(),
    IonicStorageModule.forRoot(),
    ServiceWorkerModule.register('ngsw-worker.js', {
      enabled: environment.production
    }),
  ],
  declarations: [AppComponent],
  providers: [
    InAppBrowser,
    HttpService,
    ToastService,
    LoaderService,
    AdvImageService
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}
