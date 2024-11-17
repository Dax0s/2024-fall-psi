export function readNumericInputField(fieldId: string): number | null {
  const inputElement = document.getElementById(fieldId) as HTMLInputElement;
  return !inputElement ? null : parseInt(inputElement.value);
}

export function NumericInputElement(props: {
  labelText: string;
  inputElementId: string;
  minValue: number;
  maxValue: number;
  step: number;
}) {
  return (
    <div className="flex items-center">
      <label htmlFor={props.inputElementId} className="text-lg font-medium mr-2">
        {props.labelText}
      </label>
      <input
        id={props.inputElementId}
        className="border rounded px-4 py-2 text-black"
        type="number"
        min={props.minValue}
        max={props.maxValue}
        step={props.step}
      />
    </div>
  );
}
