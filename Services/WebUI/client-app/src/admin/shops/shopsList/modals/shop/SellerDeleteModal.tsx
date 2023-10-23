import {Form, Formik} from "formik";
import {Button, FormControl, InputLabel, MenuItem, Select, Stack, Typography} from "@mui/material";
import MyTextInput from "../../../../../components/MyTextInput";
import React from "react";
import {useStore} from "../../../../../global/stores/store";
import {BusinessPriceListCreateValues} from "../../../../../global/models/priceList";
import * as yup from "yup";
import {Seller} from "../../../../../global/models/shop";

interface Props {
    sellers: Seller[]
    onDelete: (seller: Seller) => void
}

export function SellerDeleteModal({sellers, onDelete}: Props) {
    const init = {seller: ''}
    const validationSchema = yup.object({
        seller: yup.string().required( 'Sprzedawca jest wymagany'),
    });

    function handleSubmit(sellerName: string) {
        const sellerFirstName = sellerName.split(' ')[0]
        const sellerLastName = sellerName.split(' ')[1]
        const seller = sellers.find(s => s.firstName === sellerFirstName && s.lastName === sellerLastName)
        seller && onDelete(seller)
    }

    return (
        <Formik
            validationSchema={validationSchema}
            initialValues={init}
            onSubmit={(values) => handleSubmit(values.seller)}
            validateOnMount={true}>
            {({handleSubmit, handleChange, isValid, values}) => (
                <Form style={{width: '100%'}} onSubmit={handleSubmit} autoComplete='off'>
                    <Stack direction={'column'} spacing={2}
                           style={{display: 'flex', justifyContent: 'center', alignItems: 'center'}}>
                        <Typography textAlign={'center'} variant={'h4'}>Usuń sprzedawcę</Typography>
                        <FormControl fullWidth>
                            <InputLabel>Sprzedawca</InputLabel>
                            <Select
                                id={'seller'}
                                name={'seller'}
                                defaultValue={''}
                                label="Sprzedawca"
                                onChange={handleChange}
                            >
                                {sellers.map(s =>
                                    <MenuItem key={s.firstName} value={`${s.firstName} ${s.lastName}`} >{s.firstName} {s.lastName}</MenuItem>
                                )}
                            </Select>
                        </FormControl>
                        <Button disabled={!isValid} type={'submit'} onClick={() => handleSubmit} variant={'contained'}>
                            <Typography>Usuń sprzedawcę</Typography>
                        </Button>
                    </Stack>
                </Form>
            )}
        </Formik>
    );
}