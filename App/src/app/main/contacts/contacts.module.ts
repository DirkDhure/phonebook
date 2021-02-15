import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ContactsPageComponent } from './contacts-page/contacts-page.component';
import { FuseSharedModule } from '@fuse/shared.module';
import { RouterModule } from '@angular/router';
import { TranslateModule } from '@ngx-translate/core';
import { MatButtonModule, MatCheckboxModule, MatDatepickerModule, MatFormFieldModule, MatIconModule, MatInputModule, MatMenuModule, MatRippleModule, MatSelectModule, MatTableModule, MatToolbarModule } from '@angular/material';
import { FuseConfirmDialogModule, FuseSidebarModule } from '@fuse/components';
import { ContactsListComponent } from './contacts-list/contacts-list.component';
import { MainSidebarComponent } from './main-sidebar/main-sidebar.component';
import { ContactFormComponent } from './contact-form/contact-form.component';
import { ContactsSelectedBarComponent } from './contacts-selected-bar/contacts-selected-bar.component';
import { EffectsModule } from '@ngrx/effects';
import { StoreModule } from '@ngrx/store';
import { StoreModules } from 'app/store';

import * as fromContacts from '../contacts/store/reducers/contacts.reducer';
import * as fromContactsPhoneBook from '../contacts/store/reducers/phone-books.reducer';
import { ContactsEffects } from './store/effects/contacts.effects';


const routes = [
  {
    path: 'contacts',
    component: ContactsPageComponent
  }
];

@NgModule({
  declarations: [
    ContactsPageComponent,
    ContactsListComponent,
    MainSidebarComponent,
    ContactFormComponent,
    ContactsSelectedBarComponent,

  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),

    TranslateModule,

    MatButtonModule,
    MatCheckboxModule,
    MatDatepickerModule,
    MatFormFieldModule,
    MatIconModule,
    MatInputModule,
    MatMenuModule,
    MatRippleModule,
    MatTableModule,
    MatToolbarModule,

    FuseSharedModule,
    FuseConfirmDialogModule,
    FuseSidebarModule,
MatSelectModule,


    StoreModule.forFeature(StoreModules.ContactsPhoneBook, fromContactsPhoneBook.PhoneBookStateReducer),
    StoreModule.forFeature(StoreModules.ContactEntries, fromContacts.ContactsEntriesStateReducer),
    EffectsModule.forFeature([ContactsEffects])
  ],
  exports: [
    ContactsPageComponent
  ],
  entryComponents: [
    ContactFormComponent
  ]
})
export class ContactsModule { }
