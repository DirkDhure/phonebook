import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { mergeMap } from 'rxjs/internal/operators/mergeMap';
import { map, switchMap } from 'rxjs/operators';
import { ContactsService } from '../../services/contacts.service';
import { contactsLoaded } from '../actions/contacts.actions';
import { PhoneBookActions } from '../actions/phone-book.action-types';
import { phoneBookLoaded } from '../actions/phone-book.actions';


@Injectable()
export class ContactsEffects {
    loadPhoneBook$ = createEffect(() => this.actions$
        .pipe(
            ofType(PhoneBookActions.loadPhoneBook),
            mergeMap(action => this.phonebookService.loadPhoneBook(action.email)),
            switchMap((payload: any) => {
                return [
                    phoneBookLoaded({ phonebook: payload }),
                   contactsLoaded({ contacts: payload.entries })

                ];
            })
        ));


    constructor(private actions$: Actions, private phonebookService: ContactsService) { }
}
