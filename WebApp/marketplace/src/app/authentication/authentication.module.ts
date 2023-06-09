import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login/login.component';
import { FormsModule } from '@angular/forms';
import { MatDialogModule } from '@angular/material/dialog';
import { LoginDialogComponent } from './login/login-dialog/login-dialog.component';

@NgModule({
  declarations: [
    LoginComponent,
    LoginDialogComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    MatDialogModule
  ]
})
export class AuthenticationModule { }
