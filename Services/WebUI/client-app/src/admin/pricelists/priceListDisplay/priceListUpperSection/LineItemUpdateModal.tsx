import {Form, Formik} from "formik";
import {Button, FormControl, InputLabel, MenuItem, Select, Stack, Switch, Typography} from "@mui/material";
import React from "react";
import * as yup from "yup";
import {LineItem, LineItemCreateValues} from "../../../../global/models/priceList";
import {useStore} from "../../../../global/stores/store";
import MyTextInput from "../../../../components/MyTextInput";

interface Props {
    priceListId: string
    lineItem: LineItem
}

function LineItemUpdateModal({priceListId, lineItem}: Props) {
    const {adminPriceListStore, modalStore} = useStore()
    const init:LineItemCreateValues = {lineItemName: lineItem.name, newPrice: lineItem.price}
    const validationSchema = yup.object({
        lineItemName: yup.string().oneOf([lineItem.name], 'Nazwa produktu nie jest edytowalna'),
        amount: yup.number().min(0.1, 'Cena musi być dodatnia'),
        unit: yup.string().oneOf( ['Kg', 'Szt'],'Niewłaściwa jednostka'),
        currency: yup.string().oneOf( ['PLN'], 'Niewłaściwa waluta'),
    });

    async function handleFormSubmit(values: LineItemCreateValues) {
        modalStore.closeModal()
        await adminPriceListStore.updateLineItem(priceListId, values)
    }

    return (
        <Formik
            validationSchema={validationSchema}
            initialValues={init}
            onSubmit={async (values) => await handleFormSubmit(values)}
            validateOnMount={true}>
            {({handleSubmit, handleChange, isValid, values}) => (
                <Form style={{width: '100%'}} onSubmit={handleSubmit} autoComplete='off'>
                    <Stack direction={'column'} spacing={2}
                           style={{display: 'flex', justifyContent: 'center', alignItems: 'center'}}>
                        <Typography textAlign={'center'} variant={'h4'}>Aktualizacja produktu</Typography>
                        <MyTextInput
                            placeholder={'Nazwa produktu'}
                            name={'lineItemName'}
                            showErrors
                            disabled
                        />

                        <MyTextInput
                            placeholder={'Cena'}
                            name={'newPrice.amount'}
                            type={'number'}
                            showErrors
                        />
                        <FormControl fullWidth>
                            <InputLabel>Waluta</InputLabel>
                            <Select
                                id={'currency'}
                                name={'newPrice.currency.code'}
                                value={values.newPrice.currency.code}
                                label="Waluta"
                                onChange={handleChange}
                                disabled
                            >
                                <MenuItem key={1} value={'PLN'}>PLN</MenuItem>
                            </Select>
                        </FormControl>
                        <FormControl fullWidth>
                            <InputLabel>Jednostka</InputLabel>
                            <Select
                                id={'unit'}
                                name={'newPrice.unit.code'}
                                value={values.newPrice.unit!.code}
                                label="Jednostka"
                                onChange={handleChange}
                                disabled
                            >
                                <MenuItem key={1} value={'Kg'}>Kg</MenuItem>
                                <MenuItem key={2} value={'Szt'}>Szt</MenuItem>
                            </Select>
                        </FormControl>
                        <Button disabled={!isValid} type={'submit'} onClick={() => handleSubmit} variant={'contained'}>
                            <Typography>Zaktualizuj produkt</Typography>
                        </Button>
                    </Stack>
                </Form>
            )}
        </Formik>
    );
}

export default LineItemUpdateModal