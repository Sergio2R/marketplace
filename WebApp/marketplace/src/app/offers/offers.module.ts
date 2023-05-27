import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OfferItemComponent } from './offer-item/offer-item.component';
import { OfferCreationComponent } from './offer-creation/offer-creation.component';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { OfferListComponent } from './offer-list/offer-list.component';
import { MatCardModule } from '@angular/material/card';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    OfferItemComponent,
    OfferCreationComponent,
    OfferListComponent],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    BrowserModule,
    MatCardModule,
    FormsModule
  ]
})
export class OffersModule { }
