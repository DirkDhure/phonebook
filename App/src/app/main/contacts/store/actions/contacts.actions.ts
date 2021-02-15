import { createAction, props } from '@ngrx/store';
import { ContactEntryModel } from '../../models/contact-entry.model';

export const contactsLoaded = createAction('[Facilities service] Catalog products loaded', props<{ contacts: ContactEntryModel[] }>());
