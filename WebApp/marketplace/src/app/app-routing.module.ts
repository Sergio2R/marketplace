import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {LoginComponent} from './authentication/login/login.component';
import {OfferListComponent} from './offers/offer-list/offer-list.component';
import {OfferCreationComponent} from './offers/offer-creation/offer-creation.component';


const routes: Routes = [
  {path: 'Login', component: LoginComponent, pathMatch: 'full'},
  {path: 'Offer/ListItems', component: OfferListComponent, pathMatch: 'full'},
  {path: 'Offer/PostNew', component: OfferCreationComponent, pathMatch: 'full'},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
