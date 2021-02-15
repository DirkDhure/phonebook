import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { mergeMap } from 'rxjs/internal/operators/mergeMap';
import { map, switchMap } from 'rxjs/operators';
import { ContactsService } from '../../services/contacts.service';
import { ContactsActions } from '../actions/contacts.action-types';
import { contactLoaded, contactsLoaded } from '../actions/contacts.actions';
import { PhoneBookActions } from '../actions/phone-book.action-types';
import { phoneBookLoaded } from '../actions/phone-book.actions';


@Injectable()
export class ContactsEffects {
    loadPhoneBook$ = createEffect(() => this.actions$
        .pipe(
            ofType(PhoneBookActions.loadPhoneBook),
            mergeMap(action => this.phonebookService.loadPhoneBook(action.email)),
            switchMap((payload: any) => {
                this.phonebookService.phoneBook = payload;
                return [
                    phoneBookLoaded({ phonebook: payload }),
                    contactsLoaded({ contacts: payload.entries })
                ];
            })
        ));
    addContact$ = createEffect(() => this.actions$
        .pipe(
            ofType(ContactsActions.addContact),
            mergeMap((action) => this.phonebookService.addContact(action.contact)),
            switchMap((payload: any) => {
                return [
                    contactLoaded({ contact: payload.resource })

                ];
            })
        ));
    updateContact$ = createEffect(() => this.actions$
        .pipe(
            ofType(ContactsActions.updateContact),
            mergeMap((action) => this.phonebookService.updateContact(action.update.changes)),
            switchMap(() => {
                return [

                ];
            })
        ));


    constructor(private actions$: Actions, private phonebookService: ContactsService) { }
}
