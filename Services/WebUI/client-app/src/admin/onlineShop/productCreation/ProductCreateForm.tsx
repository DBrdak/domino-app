import {ProductCreateValues} from "../../../global/models/product";
import {Form, Formik} from "formik";
import { Button, FormControl, InputLabel, MenuItem, Stack, Switch, Select, Typography} from "@mui/material";
import MyTextInput from "../../../components/MyTextInput";
import React, {useEffect, useState} from "react";
import * as yup from "yup";
import PhotoUploadWidget from "../../../components/photo/PhotoUploadWidget";
import {useStore} from "../../../global/stores/store";
import {observer} from "mobx-react-lite";
import {toast} from "react-toastify";


function ProductCreateForm() {
    const {adminProductStore, adminPriceListStore, modalStore} = useStore()
    const {loading} = adminProductStore
    const [productCreateValues, setProductCreateValues] = useState(new ProductCreateValues(null));
    const [isWeightSwitchAllowed, setIsWeightSwitchAllowed] = useState(false)
    const [productNames, setProductNames] = useState<string[]>([])
    const [newPhoto, setNewPhoto] = useState<Blob | null>(null)
    const [isValidCustom, setIsValidCustom] = useState(false)

    useEffect(() => {
        const getNames = async () => setProductNames(await adminPriceListStore.getNonAggregatedProductNames())
        getNames()
    }, [])

    const validationSchema = yup.object({
        description: yup.string().required( 'Opis produktu jest wymagany'),
        subcategory: yup.string().required('Podkategoria jest wymagana'),
    });

    const handleFormSubmit = (values:ProductCreateValues) => {
        if(!(productCreateValues.category === 'Wędlina' || productCreateValues.category === 'Mięso')
            /*|| productCreateValues.name.length < 1*/) {
            toast.error('Proszę podać wszystkie wymagane dane')
            return
        }

        if(!newPhoto) {
            toast.error('Proszę dodać zdjęcie produktu')
            return
        }

        adminProductStore.setPhoto(newPhoto)
        adminProductStore.setNewProductValues({
            ...values,
            isWeightSwitchAllowed: productCreateValues.isWeightSwitchAllowed,
            category: productCreateValues.category,
            name: productCreateValues.name})
        console.log(adminProductStore.newProductValues)
        adminProductStore.addProduct()
        modalStore.closeModal()
    }

    useEffect(() => {
        setProductCreateValues({...productCreateValues, isWeightSwitchAllowed: isWeightSwitchAllowed})
    }, [isWeightSwitchAllowed])

    const handleNameSelectChange = (name: string) => {
        setProductCreateValues({...productCreateValues, name: name})
    }

    const handleCategorySelectChange = (category: string) => {
        setProductCreateValues({...productCreateValues, category: category})
    }

    useEffect(() => {
        if((productCreateValues.category === 'Wędlina' || productCreateValues.category === 'Mięso')
            && /*productCreateValues.name.length > 0 && */ newPhoto) {
            setIsValidCustom(true)
        } else {
            setIsValidCustom(false)
        }
    }, [productCreateValues])

    return (
        <Formik
            validationSchema={validationSchema}
            initialValues={productCreateValues}
            onSubmit={(values) => handleFormSubmit(values)}
            validateOnMount={true}>
            {({handleSubmit, isValid}) => (
                <Form style={{width: '100%'}} onSubmit={handleSubmit} autoComplete='off'>
                    <Stack direction={'column'} spacing={2}
                           style={{display: 'flex', justifyContent: 'center', alignItems: 'center'}}>
                        <FormControl fullWidth>
                            <InputLabel>Nazwa produktu</InputLabel>
                            <Select
                                name={'name'}
                                value={productCreateValues.name}
                                label="Nazwa produktu"
                                onChange={(e) => handleNameSelectChange(e.target.value)}
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
                        <FormControl fullWidth>
                            <InputLabel>Kategoria</InputLabel>
                            <Select
                                name={'category'}
                                value={productCreateValues.category}
                                label="Kategoria"
                                onChange={(e) => handleCategorySelectChange(e.target.value)}
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
                                checked={isWeightSwitchAllowed}
                                onChange={() => setIsWeightSwitchAllowed(!isWeightSwitchAllowed)}
                            />
                        </Stack>
                        {isWeightSwitchAllowed &&
                            <MyTextInput
                                placeholder={'Waga jednostkowa'}
                                name={'singleWeight'}
                                showErrors
                                type={'number'}
                            />
                        }
                        <PhotoUploadWidget uploadPhoto={(photo) => setNewPhoto(photo)} loading={loading} />
                        <Button disabled={adminProductStore.loading || adminPriceListStore.loading || !isValid || !isValidCustom}
                                type={'submit'} onClick={() => handleSubmit} variant={'contained'}>Dodaj produkt</Button>
                    </Stack>
                </Form>
            )}
        </Formik>
    )
}

export default observer(ProductCreateForm)