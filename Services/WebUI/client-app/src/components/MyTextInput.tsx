import React from 'react'
import { TextField, FormHelperText, FormControl, IconButton } from '@mui/material';
import { useField } from 'formik';
import { Done } from '@mui/icons-material';
import { observer } from 'mobx-react-lite';

interface Props {
  placeholder: string;
  name: string;
  label?: string;
  type?: string;
}

const MyTextInput: React.FC<Props> = (props: Props) => {
  const [field, meta, helpers] = useField(props.name);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const value = e.target.value.replace(',', '.');
    helpers.setValue(value);
  };

  return (
    <FormControl error={meta.touched && !!meta.error}>
      <TextField
        {...field}
        {...props}
        onChange={handleChange}
        label={props.label}
        variant="outlined"
        error={meta.touched && !!meta.error}
      ></TextField>
    </FormControl>
  );
}

export default observer(MyTextInput);
