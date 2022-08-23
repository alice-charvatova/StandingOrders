import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'space'
})
export class SpacePipe
        implements PipeTransform {
  transform(value: string): string {
    let result = value.replace(/\s/g, "");
    return result;
  }
}