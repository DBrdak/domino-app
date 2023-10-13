import {ProductCreateValues} from "../../../global/models/product";
import {Field, Form, Formik} from "formik";
import {Button, FormControl, InputLabel, MenuItem, Stack, Switch, Select, Typography, makeStyles} from "@mui/material";
import MyTextInput from "../../../components/MyTextInput";
import React, {useEffect, useState} from "react";
import * as yup from "yup";
import PhotoUploadWidget from "../../../components/photo/PhotoUploadWidget";
import {useStore} from "../../../global/stores/store";
import {observer} from "mobx-react-lite";
import {toast} from "react-toastify";
import LoadingComponent from "../../../components/LoadingComponent";
import {values} from "mobx";


function ProductCreateForm() {
    const {adminProductStore, adminPriceListStore, modalStore} = useStore()
    const productCreateValues = new ProductCreateValues(null)
    const [productNames, setProductNames] = useState<string[]>([])
    const [newPhoto, setNewPhoto] = useState<Blob | null>(null)

    useEffect(() => {
        const getNames = async () => setProductNames(await adminPriceListStore.getNonAggregatedProductNames())
        getNames()
    }, [])

    const validationSchema = yup.object({
        name: yup.string().required( 'Nazwa produktu jest wymagana'),
        description: yup.string().required( 'Opis produktu jest wymagany'),
        category: yup.string().oneOf( ['Mięso', 'Wędlina'], 'Niewłaściwa kategoria '),
        subcategory: yup.string().required('Podkategoria jest wymagana'),
    });

    const handleFormSubmit = async (values:ProductCreateValues) => {
        if(!newPhoto) {
            toast.error('Proszę dodać zdjęcie produktu')
            return
        }
        console.log(values)
        adminProductStore.setPhoto(newPhoto)
        adminProductStore.setNewProductValues(values)
        modalStore.closeModal()
        await adminProductStore.addProduct()
    }

    return (
        <Formik
            validationSchema={validationSchema}
            initialValues={productCreateValues}
            onSubmit={async (values) => await handleFormSubmit(values)}
            validateOnMount={true}>
            {({handleSubmit, handleChange, isValid, values}) => (
                <Form style={{width: '100%'}} onSubmit={handleSubmit} autoComplete='off'>
                    <Stack direction={'column'} spacing={2}
                           style={{display: 'flex', justifyContent: 'center', alignItems: 'center'}}>
                        <FormControl fullWidth>
                            <InputLabel>Nazwa produktu</InputLabel>
                            <Select
                                id={'name'}
                                name={'name'}
                                value={values.name}
                                label="Nazwa produktu"
                                onChange={handleChange}
                            >
                                {productNames.map(n => (
                                    <MenuItem key={n} value={n}>{n}</MenuItem>
                                ))}
                            </Select>
                        </FormControl>
                        <MyTextInput
                            placeholder={'Opis produktu'}
                            name={'description'}
                            showErrors
                        />
                        <FormControl fullWidth >
                            <InputLabel>Kategoria</InputLabel>
                            <Select
                                id={'name'}
                                name={'category'}
                                value={values.category}
                                label="Kategoria"
                                onChange={handleChange}
                            >
                                <MenuItem value='Wędlina'>Wędlina</MenuItem>
                                <MenuItem value='Mięso'>Mięso</MenuItem>
                            </Select>
                        </FormControl>
                        <MyTextInput
                            placeholder={'Podkategoria produktu'}
                            name={'subcategory'}
                            showErrors
                        />
                        <Stack direction={'row'} spacing={2} style={{display: 'flex', justifyContent: 'start', alignItems: 'center'}}>
                            <Typography variant={'h6'}>Jednostka alternatywna?</Typography>
                            <Switch
                                name={'isWeightSwitchAllowed'}
                                checked={values.isWeightSwitchAllowed}
                                onChange={handleChange}
                            />
                        </Stack>
                        {values.isWeightSwitchAllowed &&
                            <MyTextInput
                                placeholder={'Waga jednostkowa'}
                                name={'singleWeight'}
                                showErrors
                                type={'number'}
                            />
                        }
                        <PhotoUploadWidget uploadPhoto={(photo) => setNewPhoto(photo)} />
                        <Button
                            disabled={!isValid || !newPhoto}
                            type={'submit'} onClick={() => handleSubmit} variant={'contained'}>
                            {adminProductStore.loading || adminPriceListStore.loading ?
                                <LoadingComponent /> : <Typography>Dodaj produkt</Typography>
                            }
                        </Button>
                    </Stack>
                </Form>
            )}
        </Formik>
    )
}

export default observer(ProductCreateForm)