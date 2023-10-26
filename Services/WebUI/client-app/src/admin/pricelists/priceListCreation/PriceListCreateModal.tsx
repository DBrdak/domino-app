import {Form, Formik} from "formik";
import {Button, FormControl, InputLabel, MenuItem, Select, Stack, Switch, Typography} from "@mui/material";
import MyTextInput from "../../../components/MyTextInput";
import PhotoUploadWidget from "../../../components/photo/PhotoUploadWidget";
import LoadingComponent from "../../../components/LoadingComponent";
import React from "react";
import {BusinessPriceListCreateValues} from "../../../global/models/priceList";
import * as yup from "yup";
import {useStore} from "../../../global/stores/store";

function PriceListCreateModal() {
    //TODO w przyszłości dać kontrahentów z dropdowna
    const {adminPriceListStore, modalStore} = useStore()
    const init: BusinessPriceListCreateValues = {name: '', contractorName: '', category: ''}
    const validationSchema = yup.object({
        name: yup.string().required( 'Nazwa cennika jest wymagana'),
        contractorName: yup.string().required( 'Kontrahent jest wymagany'),
    });

    async function handleFormSubmit(values: BusinessPriceListCreateValues) {
        modalStore.closeModal()
        await adminPriceListStore.createBusinessPriceList(values)
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
                        <Typography textAlign={'center'} variant={'h4'}>Nowy cennik</Typography>
                        <MyTextInput
                            placeholder={'Nazwa cennika'}
                            name={'name'}
                            showErrors
                        />
                        <MyTextInput
                            placeholder={'Kontrahent'}
                            name={'contractorName'}
                            showErrors
                        />
                        <FormControl fullWidth>
                            <InputLabel>Nazwa produktu</InputLabel>
                            <Select
                                id={'category'}
                                name={'category'}
                                value={values.category}
                                label="Kategoria"
                                onChange={handleChange}
                            >
                                <MenuItem key={1} value={'Mięso'}>Mięso</MenuItem>
                                <MenuItem key={2} value={'Wędliny'}>Wędliny</MenuItem>
                            </Select>
                        </FormControl>
                        <Button disabled={!isValid} type={'submit'} onClick={() => handleSubmit} variant={'contained'}>
                            <Typography>Dodaj cennik</Typography>
                        </Button>
                    </Stack>
                </Form>
            )}
        </Formik>
    );
}

export default PriceListCreateModal