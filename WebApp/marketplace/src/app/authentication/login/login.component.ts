import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/core/marketplace-api/models/user.model';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { MarketplaceApiService } from 'src/app/core/marketplace-api/marketplace-api.service';
import { Router } from '@angular/router';
import { LoginDialogComponent } from './login-dialog/login-dialog.component';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  user: User = new User('', 0);
  dialogRef: MatDialogRef<any>;

  constructor(
    public router: Router,
    public dialog: MatDialog,
    public marketplaceApiService: MarketplaceApiService,
  ) {
    this.logout();
  }


  ngOnInit(): void {
  }

  logout(): void {
    localStorage.removeItem('username');
    localStorage.removeItem('userid');
  }

  onSubmit(): void {
    const username = this.user.username;

    this.marketplaceApiService.getUserIdByUsername(username)
      .then(userId => {
        if (userId) {
          this.user.userid = userId;
          this.loginSuccess()
        } else {
          this.dialog.open(LoginDialogComponent, {
            data: { errorMessage: 'Invalid username or password' }
          });
        }
      })
      .catch(error => {
        console.error('An error occurred while validating user existence:', error);
      });
  }

  loginSuccess() {
    setTimeout(() => {
      localStorage.setItem("username", this.user.username);
      localStorage.setItem("userid", this.user.userid.toString());
      this.router.navigate(['/']);
    }, 250);
  }

}
