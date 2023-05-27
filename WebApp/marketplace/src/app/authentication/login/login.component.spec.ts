import { ComponentFixture, TestBed } from '@angular/core/testing';
import { LoginComponent } from './login.component';
import { FormsModule } from '@angular/forms';
import { MatDialogModule } from '@angular/material/dialog';
import { RouterTestingModule } from '@angular/router/testing';
import { MarketplaceApiService } from 'src/app/core/marketplace-api/marketplace-api.service';

describe('LoginComponent', () => {
  let component: LoginComponent;
  let fixture: ComponentFixture<LoginComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [LoginComponent],
      imports: [FormsModule, MatDialogModule, RouterTestingModule],
      providers: [MarketplaceApiService],
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LoginComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should clear local storage on logout', () => {
    localStorage.setItem('username', 'testuser');
    localStorage.setItem('userid', '123');

    component.logout();

    expect(localStorage.getItem('username')).toBeNull();
    expect(localStorage.getItem('userid')).toBeNull();
  });
});
