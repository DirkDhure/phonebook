import { ContactEntryModel } from './contact-entry.model';

export class BookDetailsModel {
    id: string;
    ownerEmail: string;
    name: string;
    dateCreated: Date;
    entries: ContactEntryModel[];
}
