import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'stringTrim'
})
export class StringTrimPipe implements PipeTransform {

  transform(value: string, args?: number): string {

    let result = value;

    if (result.length > args) {
      result = `${result.substr(0, args - 3)}...`;
    }

    return result;
  }

}
