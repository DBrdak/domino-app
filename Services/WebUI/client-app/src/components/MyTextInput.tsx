import React from 'react';
import { TextField, FormControl, Tooltip, FilledInputProps, InputProps, OutlinedInputProps } from '@mui/material';
import { useField } from 'formik';
import { observer } from 'mobx-react-lite';

interface Props {
    placeholder: string;
    name: string;
    showErrors?: any;
    label?: string;
    type?: string;
    maxValue?: number;
    minValue?: number;
    inputProps?: Partial<FilledInputProps> | Partial<OutlinedInputProps> | Partial<InputProps>
    style?: React.CSSProperties
}

const MyTextInput: React.FC<Props> = ({ showErrors, maxValue, minValue, inputProps, type, style, ...props }) => {
    const [field, meta, helpers] = useField(props.name);

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        let value = e.target.value.replace(',', '.');

        if (type === 'number') {
            value = value.replace(/[^0-9.]/g, '');
        }

        if (value !== '0') {
            value = value.replace(/^0+/, '');
        }

        if (value.length > 0) {
            value = value.charAt(0).toUpperCase() + value.slice(1);
        }

        if (maxValue && parseFloat(value) > maxValue) {
            value = maxValue.toString();
        }

        if (minValue && parseFloat(value) < minValue) {
            value = minValue.toString();
        }

        helpers.setValue(value);
    };

    return (
    showErrors ? (
        <FormControl error={meta.touched && !!meta.error} fullWidth>
            <Tooltip title={meta.touched && meta.error ? meta.error : ''} placement="right">
                <TextField
                    {...field}
                    {...props}
                    onChange={handleChange}
                    label={props.label}
                    variant="outlined"
                    error={meta.touched && !!meta.error}
                    InputProps={inputProps} 
                    style={style}
                />
            </Tooltip>
        </FormControl>
    ) : (
        <FormControl error={meta.touched && !!meta.error} fullWidth>
            <TextField
                {...field}
                {...props}
                onChange={handleChange}
                label={props.label}
                variant="outlined"
                error={meta.touched && !!meta.error}
                InputProps={inputProps} 
                style={style}
            />
        </FormControl>
    )
    );
}

export default observer(MyTextInput);
