import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'currencyFormat'
})

export class CurrencyFormatPipe
        implements PipeTransform {
  transform(value: string, arg: string): string {
    if (value == null) {
      return value;
    }
    return value.replace(arg, '€ -'); 
  }
}