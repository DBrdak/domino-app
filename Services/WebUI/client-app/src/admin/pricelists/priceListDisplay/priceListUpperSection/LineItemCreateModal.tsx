import {Form, Formik} from "formik";
import {Button, FormControl, InputLabel, MenuItem, Select, Stack, Switch, Typography} from "@mui/material";
import React from "react";
import * as yup from "yup";
import {LineItemCreateValues} from "../../../../global/models/priceList";
import {useStore} from "../../../../global/stores/store";
import MyTextInput from "../../../../components/MyTextInput";

interface Props {
    priceListId: string
}

function LineItemCreateModal({priceListId}: Props) {
    const {adminPriceListStore, modalStore} = useStore()
    const init: LineItemCreateValues = {lineItemName: '', newPrice: {amount: 0, unit: {code: 'Kg'}, currency: {code: 'PLN'}}}
    const validationSchema = yup.object({
        lineItemName: yup.string().required( 'Nazwa cennika jest wymagana'),
        amount: yup.number().positive('Cena musi być dodatnia'),
        unit: yup.string().oneOf( ['Kg', 'Szt'],'Niewłaściwa jednostka'),
        currency: yup.string().oneOf( ['PLN'], 'Niewłaściwa waluta'),
    });

    async function handleFormSubmit(values: LineItemCreateValues) {
        modalStore.closeModal()
        await adminPriceListStore.createLineItem(priceListId, values)
    }

    return (
        <Formik
            validationSchema={validationSchema}
            initialValues={{lineItemName: init.lineItemName, amount: init.newPrice.amount, currency: init.newPrice.currency.code, unit: init.newPrice.unit?.code}}
            onSubmit={async (values) =>
                await handleFormSubmit({lineItemName: values.lineItemName, newPrice: {amount: values.amount,
                        currency: {code: values.currency}, unit: {code: values.unit!}}})}
            validateOnMount={true}>
            {({handleSubmit, handleChange, isValid, values}) => (
                <Form style={{width: '100%'}} onSubmit={handleSubmit} autoComplete='off'>
                    <Stack direction={'column'} spacing={2}
                           style={{display: 'flex', justifyContent: 'center', alignItems: 'center'}}>
                        <Typography textAlign={'center'} variant={'h4'}>Nowy produkt</Typography>
                        <MyTextInput
                            placeholder={'Nazwa produktu'}
                            name={'lineItemName'}
                            showErrors
                        />

                        <MyTextInput
                            placeholder={'Cena'}
                            name={'amount'}
                            type={'number'}
                            showErrors
                        />
                        <FormControl fullWidth>
                            <InputLabel>Waluta</InputLabel>
                            <Select
                                id={'currency'}
                                name={'currency'}
                                value={values.currency}
                                label="Waluta"
                                onChange={handleChange}
                            >
                                <MenuItem key={1} value={'PLN'}>PLN</MenuItem>
                            </Select>
                        </FormControl>
                        <FormControl fullWidth>
                            <InputLabel>Jednostka</InputLabel>
                            <Select
                                id={'unit'}
                                name={'unit'}
                                value={values.unit}
                                label="Jednostka"
                                onChange={handleChange}
                            >
                                <MenuItem key={1} value={'Kg'}>Kg</MenuItem>
                                <MenuItem key={2} value={'Szt'}>Szt</MenuItem>
                            </Select>
                        </FormControl>
                        <Button disabled={!isValid} type={'submit'} onClick={() => handleSubmit} variant={'contained'}>
                            <Typography>Dodaj produkt</Typography>
                        </Button>
                    </Stack>
                </Form>
            )}
        </Formik>
    );
}

export default LineItemCreateModal