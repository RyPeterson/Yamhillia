import { useState, useCallback, FormEventHandler } from "react";

export default function useInput(
  initialValue: string
): [string, FormEventHandler<HTMLInputElement>, (value: string) => void] {
  const [value, setValue] = useState<string>(initialValue);
  const onChange: FormEventHandler<HTMLInputElement> = useCallback(event => {
    setValue(event.currentTarget.value);
  }, []);

  return [value, onChange, setValue];
}
