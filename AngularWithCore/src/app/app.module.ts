import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './component/login/login.component';
import { HomeComponent } from './component/pages/home/home.component';
import { HeaderComponent } from './component/pages/home/component/header/header.component';
import { BasicInfoComponent } from './component/pages/home/component/basic-info/basic-info.component';
import { MenuComponent } from './component/pages/home/component/menu/menu.component';
import { MenuItemComponent } from './component/pages/home/component/menu-item/menu-item.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    HomeComponent,
    HeaderComponent,
    BasicInfoComponent,
    MenuComponent,
    MenuItemComponent    
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    ReactiveFormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
