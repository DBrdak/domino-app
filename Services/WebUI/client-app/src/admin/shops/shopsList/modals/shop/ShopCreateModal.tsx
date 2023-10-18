import {Product} from "../../../../../global/models/product";
import React, {useEffect, useState} from "react";
import * as yup from "yup";
import {Form, Formik} from "formik";
import {
    Button,
    FormControl,
    InputLabel,
    MenuItem,
    Select,
    SelectChangeEvent,
    Stack,
    Typography
} from "@mui/material";
import MyTextInput from "../../../../../components/MyTextInput";
import LoadingComponent from "../../../../../components/LoadingComponent";
import {Shop, ShopCreateValues} from "../../../../../global/models/shop";
import LocationPicker from "../../../../components/LocationPicker";
import {useStore} from "../../../../../global/stores/store";
import {Location} from '../../../../../global/models/common'
import {toast} from "react-toastify";
import {observer} from "mobx-react-lite";

interface FormValues {
    shopName: string,
    vehiclePlateNumber: string | null,
    location: Location | null
}

const ShopCreateModal: React.FC = () => {
    const {adminShopStore, modalStore} = useStore()
    const [shopType, setShopType] = useState<string>('')
    const shop:Shop = {shopName: '', sellers: [], id: ''}
    
    const validate = (values: FormValues, toasting: boolean) => {
        if(values.shopName.length < 1) {
            toasting && toast.error('Nazwa sklepu jest wymagana')
            return false
        } if(shopType === 'stationary' && (values.location!.longitude.length! < 1 || values.location!.latitude.length! < 1 || values.location!.name.length! < 1)) {
            toasting && toast.error('Lokalizacja sklepu stacjonarnego jest wymagana')
            return false
        } if(shopType === 'mobile' && !values.vehiclePlateNumber) {
            toasting && toast.error('Numery rejestracyjne sklepu obwoźnego są wymagane')
            return false
        } if(shopType === 'mobile' && !values.vehiclePlateNumber?.match(/[A-Z]{2,3}\s[A-Z0-9]{4,5}/)) {
            toasting && toast.error('Nieprawidłowy format numeru rejestracyjnego pojazdu')
            return false
        } if(shopType === '') {
            toasting && toast.error('Rodzaj sklepu jest wymagany')
            return false
        }
        return true
    }

    const handleSubmit = (values: FormValues) => {
        const validationSuccess = validate(values,true)

        if(!validationSuccess) {
            return
        }

        shopType === 'stationary' && adminShopStore.setStationaryShopCreateValues(values.shopName, values.location!)
        shopType === 'mobile' && adminShopStore.setMobileShopCreateValues(values.shopName, values.vehiclePlateNumber!)
        modalStore.closeModal()
        adminShopStore.createShop()
    }

    return (
        <Formik
            initialValues={{shopName: shop.shopName, vehiclePlateNumber: '',  location: {longitude: '', latitude: '', name: ''}}}
            onSubmit={(values) => handleSubmit(values)}>
            {({ handleSubmit, values, handleChange }) => (
                <Form>
                    <Stack direction={'column'} gap={3} style={{display: 'flex', justifyContent: 'center', alignItems: 'center'}}>
                        <Typography variant="h4" style={{display: 'flex', justifyContent: 'space-between', alignItems: 'center'}} height={'75px'}>
                            Nowy sklep
                        </Typography>
                        <MyTextInput name='shopName' placeholder={'Nazwa sklepu'} label='Nazwa sklepu' />
                        {values.shopName &&
                            <FormControl fullWidth>
                                <InputLabel>Rodzaj sklepu</InputLabel>
                                <Select
                                    id={'shopType'}
                                    name={'shopType'}
                                    label="Rodzaj sklepu"
                                    defaultValue={''}
                                    onChange={(e: SelectChangeEvent<string>) => setShopType(e.target.value)}
                                >
                                    <MenuItem value='stationary'>Stacjonarny</MenuItem>
                                    <MenuItem value='mobile'>Obwoźny</MenuItem>
                                </Select>
                            </FormControl>
                        }
                        {shopType === 'stationary' &&
                            <LocationPicker
                                locationName={values.shopName}
                                onChange={(pickedLocation) => {
                                    values.location = pickedLocation
                                    console.log(values.location)
                                }}
                            />}
                        {shopType === 'mobile' &&
                            <MyTextInput name='vehiclePlateNumber' placeholder={'Numer rejestracyjny pojazdu'}
                                         label='Numer rejestracyjny pojazdu'/>}
                        <Button
                            disabled={adminShopStore.loading}
                            type={'submit'} onClick={() => handleSubmit} variant={'contained'}>
                            {adminShopStore.loading ?
                                <LoadingComponent /> : <Typography>Dodaj</Typography>
                            }
                        </Button>
                    </Stack>
                </Form>
            )}
        </Formik>
    );
}

export default observer(ShopCreateModal)