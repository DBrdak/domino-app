import * as yup from "yup";
import {Form, Formik} from "formik";
import {Button, FormControl, InputLabel, MenuItem, Select, Stack, Typography} from "@mui/material";
import React from "react";
import {SalePoint} from "../../../../../global/models/shop";

interface Props {
    salePoints: SalePoint[],
    onSubmit: (salePointToEnable: SalePoint) => void
}

export function MobileShopSalePointEnableModal({salePoints, onSubmit}: Props) {
    const init = {salePoint: ''}

    function handleSubmit(salePointName: string) {
        const salePoint = salePoints.find(sp => sp.location.name === salePointName)
        salePoint && onSubmit(salePoint)
    }

    return (
        <Formik
            initialValues={init}
            onSubmit={(values) => handleSubmit(values.salePoint)}
            validateOnMount={true}>
            {({handleSubmit, handleChange, isValid, values}) => (
                <Form style={{width: '100%'}} onSubmit={handleSubmit} autoComplete='off'>
                    <Stack direction={'column'} spacing={2}
                           style={{display: 'flex', justifyContent: 'center', alignItems: 'center'}}>
                        <Typography textAlign={'center'} variant={'h4'}>Odblokuj punkt sprzedaży</Typography>
                        <FormControl fullWidth required>
                            <InputLabel>Punkt sprzedaży</InputLabel>
                            <Select
                                name={'salePoint'}
                                defaultValue={''}
                                label="Punkt sprzedaży"
                                onChange={handleChange}
                            >
                                {salePoints.map(sp =>
                                    <MenuItem key={sp.location.name} value={sp.location.name} >{sp.location.name}</MenuItem>
                                )}
                            </Select>
                        </FormControl>
                        <Button type={'submit'} onClick={() => handleSubmit} variant={'contained'}>
                            <Typography>Odblokuj</Typography>
                        </Button>
                    </Stack>
                </Form>
            )}
        </Formik>
    );
}