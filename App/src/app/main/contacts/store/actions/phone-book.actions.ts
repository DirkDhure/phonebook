import { createAction, props } from '@ngrx/store';
import { BookDetailsModel } from '../../models/book-detail.model';

export const loadPhoneBook = createAction('Load phone book', props<{ email: string }>());
export const phoneBookLoaded = createAction('Phone book loaded', props<{ phonebook: BookDetailsModel }>());
