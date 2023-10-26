import {MobileShop, SalePoint, Seller} from "../../../../../global/models/shop";
import * as yup from "yup";
import {Form, Formik} from "formik";
import {Button, FormControl, InputLabel, MenuItem, Select, Stack, Typography} from "@mui/material";
import MyTextInput from "../../../../../components/MyTextInput";
import React, {useState} from "react";
import LocationPicker from "../../../../../components/LocationPicker";
import {LocalizationProvider, TimeField} from "@mui/x-date-pickers";
import { AdapterDateFns } from '@mui/x-date-pickers/AdapterDateFns';
import pl from 'date-fns/locale/pl';
import {TimeRange, Location} from "../../../../../global/models/common";

interface Props {
    shop: MobileShop
    onSubmit: (newSalePoint: SalePoint) => void
    existingSalePoints: SalePoint[]
}

export function MobileShopNewSalePointModal({shop, onSubmit, existingSalePoints}: Props) {
    const [pickedLocation, setPickedLocation] = useState<Location | null>(null)
    const [isNameEditable, setIsNameEditable] = useState(true)

    const init: SalePoint = {
        id: '',
        location: {latitude: '', longitude: '', name: ''},
        cachedOpenHours: null,
        openHours: {start: '', end: ''},
        isClosed: false,
        weekDay: {value: ''}}

    const validateTime = (timeRange: TimeRange) => {
        const start = timeRange.start
        const end = timeRange.end
        const isHourValid = start.split(':')[0] <= end.split(':')[0]
        const isHoursEqual = start.split(':')[0] === end.split(':')[0]
        const isMinutesValid = isHoursEqual ? start.split(':')[1] < end.split(':')[1] : true

        return isHourValid && isMinutesValid
    }

    const dayWeekUnavailable = (weekDayValue: string) => {
        const existingSalePointInPickedLocation = pickedLocation &&
            shop.salePoints.find(sp => sp.location === pickedLocation)

        const shopWeekDayValueForExistingLocation = existingSalePointInPickedLocation && existingSalePointInPickedLocation.weekDay.value

        if(shopWeekDayValueForExistingLocation){
            return false
        }

        return shopWeekDayValueForExistingLocation === weekDayValue
    }

    return <Formik
        initialValues={init}
        onSubmit={(values) => onSubmit(values)}
        validateOnMount={true}>
        {({handleSubmit, handleChange, isValid, values, setValues}) => <Form style={{width: '100%'}} onSubmit={handleSubmit} autoComplete='off'>
            <Stack direction={'row'} spacing={3}>
                <Stack direction={'column'} spacing={2}
                       style={{display: 'flex', justifyContent: 'center', alignItems: 'center'}}>
                    <Typography textAlign={'center'} variant={'h4'}>Nowy punkt sprzedaży</Typography>
                    <MyTextInput
                        inputProps={{required: true, disabled: !isNameEditable}}
                        placeholder={'Nazwa punktu *'}
                        name={'location.name'}
                        label="Nazwa punktu *"
                        showErrors
                    />
                    <FormControl fullWidth required>
                        <InputLabel>Dzień tygodnia</InputLabel>
                        <Select
                            name={'weekDay.value'}
                            value={values.weekDay.value}
                            label="Dzień tygodnia"
                            onChange={handleChange}
                        >
                            <MenuItem key={1} value={'Poniedziałek'} disabled={dayWeekUnavailable('Poniedziałek')}>
                                Poniedziałek
                            </MenuItem>
                            <MenuItem key={2} value={'Wtorek'} disabled={dayWeekUnavailable('Poniedziałek')}>
                                Wtorek
                            </MenuItem>
                            <MenuItem key={3} value={'Środa'} disabled={dayWeekUnavailable('Poniedziałek')}>
                                Środa
                            </MenuItem>
                            <MenuItem key={4} value={'Czwartek'} disabled={dayWeekUnavailable('Poniedziałek')}>
                                Czwartek
                            </MenuItem>
                            <MenuItem key={5} value={'Piątek'} disabled={dayWeekUnavailable('Poniedziałek')}>
                                Piątek
                            </MenuItem>
                            <MenuItem key={6} value={'Sobota'} disabled={dayWeekUnavailable('Poniedziałek')}>
                                Sobota
                            </MenuItem>
                            <MenuItem key={7} value={'Niedziela'} disabled={dayWeekUnavailable('Poniedziałek')}>
                                Niedziela
                            </MenuItem>
                        </Select>
                    </FormControl>
                    <LocalizationProvider dateAdapter={AdapterDateFns} adapterLocale={pl}>
                        <TimeField
                            required
                            label="Początek sprzedaży"
                            name={'openHours.start'}
                            onChange={(date) => {
                                setValues({
                                        ...values,
                                        openHours: {
                                            end: values.openHours ? values.openHours.end : '',
                                            start: (date as Date) ? (date as Date).toLocaleTimeString() : ''
                                }})
                            }}
                        />
                        <TimeField
                            required
                            label="Koniec sprzedaży"
                            name={'openHours.end'}
                            onChange={(date) => {
                                setValues({
                                        ...values,
                                        openHours: {
                                            start: values.openHours ? values.openHours.start : '',
                                            end: (date as Date) ? (date as Date).toLocaleTimeString() : ''
                                }})
                            }}
                        />
                    </LocalizationProvider>
                    <Button disabled={values.location.latitude.length < 1 || !validateTime(values.openHours!)}
                            type={'submit'} onClick={() => handleSubmit} variant={'contained'}>
                        <Typography>Dodaj punkt</Typography>
                    </Button>
                </Stack>
                <LocationPicker locationName={values.location.name} existingSalePoints={existingSalePoints}
                                setNameEdition={(state) => setIsNameEditable(state)}
                                onChange={(pickedLocation) => {
                                    setValues({...values, location: pickedLocation})
                                    setPickedLocation(pickedLocation)
                                }} newMode />
            </Stack>
        </Form>}
    </Formik>;
}