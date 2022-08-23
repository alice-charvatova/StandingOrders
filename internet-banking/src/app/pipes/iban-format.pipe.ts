import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'iban'
})

export class IbanFormatPipe
        implements PipeTransform {
  transform(value: string): string {
    let formatedIban = "";
    for (var i = 0; i <= 5; i++) {
        formatedIban += value.substr(i*4,4) + " ";
    }
    return formatedIban.substring(0, formatedIban.length - 1);
  }
}