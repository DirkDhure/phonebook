import { NgModule } from '@angular/core';
import { ContactsStateModule } from 'app/main/contacts/services/contacts-state.module';
import 'hammerjs';



@NgModule({
    imports: [
        ContactsStateModule
    ],
    exports: [
        ContactsStateModule
    ]

})
export class StateModule {
    constructor(
    ) {

    }
}
