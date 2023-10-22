import {Form, Formik} from "formik";
import {Button, FormControl, InputLabel, MenuItem, Select, Stack, Typography} from "@mui/material";
import React from "react";
import MyTextInput from "../../../../../components/MyTextInput";
import * as yup from "yup";

interface Props {
    vehiclePlateNumber: string
    onSubmit: (newVehiclePlateNumber: string) => void
}

export function MobileShopVehicleUpdate({vehiclePlateNumber, onSubmit}:Props) {

    const validationSchema = yup.object({
        vehiclePlateNumber: yup.string()
            .required( 'Numer rejestracyjny jest wyamagany')
            .matches(/[A-Z]{2,3}\s[A-Z0-9]{4,5}/, 'Numer rejestracyjny w złym formacie')
    });

    function handleSubmit(plateNumber: string) {
        plateNumber && onSubmit(plateNumber)
    }

    return (
        <Formik
            initialValues={{vehiclePlateNumber}}
            validationSchema={validationSchema}
            onSubmit={(values) => handleSubmit(values.vehiclePlateNumber)}
            validateOnMount={true}>
            {({handleSubmit, handleChange, isValid, values}) => (
                <Form style={{width: '100%'}} onSubmit={handleSubmit} autoComplete='off'>
                    <Stack direction={'column'} spacing={2}
                           style={{display: 'flex', justifyContent: 'center', alignItems: 'center'}}>
                        <Typography textAlign={'center'} variant={'h4'}>Odblokuj punkt sprzedaży</Typography>
                        <MyTextInput
                            name={'vehiclePlateNumber'}
                            capitalize
                            placeholder={'Numer rejestracyjny'}
                            label={'Numer rejestracyjny'}
                            showErrors
                            maxLength={9}
                        />
                        <Button type={'submit'} onClick={() => handleSubmit} variant={'contained'}>
                            <Typography>Zaktualizuj</Typography>
                        </Button>
                    </Stack>
                </Form>
            )}
        </Formik>
    );
}