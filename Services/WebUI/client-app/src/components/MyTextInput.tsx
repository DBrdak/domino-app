  import React from 'react'
  import { TextField, FormHelperText, FormControl, IconButton } from '@mui/material';
  import { useField } from 'formik';
  import { Done } from '@mui/icons-material';
  import { observer } from 'mobx-react-lite';

  interface Props {
    placeholder: string;
    name: string;
    showErrors?: any
    label?: string;
    type?: string;
  }

  const MyTextInput: React.FC<Props> = ({ showErrors, ...props }) => {
    const [field, meta, helpers] = useField(props.name);

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
      let value = e.target.value.replace(',', '.');

      if (value !== '0') {
        value = value.replace(/^0+/, '');
      }

      if (value.length > 0) {
        value = value.charAt(0).toUpperCase() + value.slice(1);
      }

      helpers.setValue(value);
    };

    return (
      showErrors ?
        <FormControl error={meta.touched && !!meta.error} fullWidth>
          <TextField
          {...field}
          {...props}
          onChange={handleChange}
          label={props.label}
          variant="outlined"
          error={meta.touched && !!meta.error}
          helperText={meta.touched ? meta.error : undefined}
          ></TextField>
        </FormControl>
      :
        <FormControl error={meta.touched && !!meta.error} fullWidth>
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
