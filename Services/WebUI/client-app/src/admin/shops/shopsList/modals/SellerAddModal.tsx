import {useStore} from "../../../../global/stores/store";
import {BusinessPriceListCreateValues} from "../../../../global/models/priceList";
import * as yup from "yup";
import {Form, Formik} from "formik";
import {Button, Stack, Typography} from "@mui/material";
import MyTextInput from "../../../../components/MyTextInput";
import React from "react";
import {Seller} from "../../../../global/models/shop";

interface Props {
    onSubmit: (seller: Seller) => void
}

export function SellerAddModal({onSubmit}: Props) {
    const init: Seller = {firstName: '', lastName: '', phoneNumber: ''}
    const validationSchema = yup.object({
        firstName: yup.string().required( 'Imię sprzedawcy jest wymagane'),
        lastName: yup.string().required( 'Nazwisko sprzedawcy jest wymagane'),
        phoneNumber: yup.string().optional(),
    });

    return (
        <Formik
            validationSchema={validationSchema}
            initialValues={init}
            onSubmit={(values) => onSubmit(values)}
            validateOnMount={true}>
            {({handleSubmit, handleChange, isValid, values}) => (
                <Form style={{width: '100%'}} onSubmit={handleSubmit} autoComplete='off'>
                    <Stack direction={'column'} spacing={2}
                           style={{display: 'flex', justifyContent: 'center', alignItems: 'center'}}>
                        <Typography textAlign={'center'} variant={'h4'}>Nowy sprzedawca</Typography>
                        <MyTextInput
                            placeholder={'Imię'}
                            name={'firstName'}
                            showErrors
                        />
                        <MyTextInput
                            placeholder={'Nazwisko'}
                            name={'lastName'}
                            showErrors
                        />
                        <MyTextInput
                            placeholder={'Numer telefonu'}
                            name={'phoneNumber'}
                            showErrors
                        />
                        <Button disabled={!isValid} type={'submit'} onClick={() => handleSubmit} variant={'contained'}>
                            <Typography>Dodaj sprzedawcę</Typography>
                        </Button>
                    </Stack>
                </Form>
            )}
        </Formik>
    );
}