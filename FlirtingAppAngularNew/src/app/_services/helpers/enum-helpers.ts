import { ILookupOption } from 'src/app/_models/lookup-option';

export function enumToKeyValueArray<T>(enumObj: any): ILookupOption<T>[] {
  const result = new Array<ILookupOption<T>>();
  for (const key in enumObj) {
    if (isNaN(Number(key))) {
      result.push({ id: enumObj[key], value: key });
    }
  }
  return result;
}
