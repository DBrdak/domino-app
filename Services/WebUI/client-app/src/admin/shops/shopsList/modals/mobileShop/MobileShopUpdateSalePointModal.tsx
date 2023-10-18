import {SalePoint} from "../../../../../global/models/shop";
import {Form, Formik, FormikErrors} from "formik";
import {Button, FormControl, InputLabel, MenuItem, Select, Stack, Tooltip, Typography} from "@mui/material";
import React, {useState} from "react";
import {TimeRange, Location} from "../../../../../global/models/common";
import MyTextInput from "../../../../../components/MyTextInput";
import {LocalizationProvider, TimeField} from "@mui/x-date-pickers";
import {AdapterDateFns} from "@mui/x-date-pickers/AdapterDateFns";
import pl from "date-fns/locale/pl";
import LocationPicker from "../../../../components/LocationPicker";


interface Props {
    salePoints: SalePoint[],
    onSubmit: (salePointToDelete: SalePoint) => void
}

export function MobileShopUpdateSalePointModal({salePoints, onSubmit}: Props) {
    const init: SalePoint = {
        location: {latitude: '', longitude: '', name: ''},
        cachedOpenHours: {start: '', end: ''},
        openHours: {start: '', end: ''},
        isClosed: false,
        weekDay: {value: ''}}

    const validateTime = (values: SalePoint) => {
        const start = values.openHours?.start ?? values.cachedOpenHours?.start
        const end = values.openHours?.end ?? values.cachedOpenHours?.start

        if(!start || !end) {
            return false
        }

        const isHourValid = start.split(':')[0] <= end.split(':')[0]
        const isHoursEqual = start.split(':')[0] === end.split(':')[0]
        const isMinutesValid = isHoursEqual ? start.split(':')[1] < end.split(':')[1] : true

        return isHourValid && isMinutesValid
    }

    const time = (selectedSalePoint: SalePoint) => {
        return [
            new Date(0,0,1,
                Number(selectedSalePoint.openHours?.start.split(':')[0]),
                Number(selectedSalePoint.openHours?.start.split(':')[1])),
            new Date(0,0,1,
                Number(selectedSalePoint.openHours?.end.split(':')[0]),
                Number(selectedSalePoint.openHours?.end.split(':')[1])),
            new Date(0,0,1,
                Number(selectedSalePoint.cachedOpenHours?.start.split(':')[0]),
                Number(selectedSalePoint.cachedOpenHours?.start.split(':')[1])),
            new Date(0,0,1,
                Number(selectedSalePoint.cachedOpenHours?.end.split(':')[0]),
                Number(selectedSalePoint.cachedOpenHours?.end.split(':')[1]))
        ]
    }

    function handleLocationPick(pickedLocation: Location, setValues: (values: React.SetStateAction<SalePoint>, shouldValidate?: (boolean | undefined)) => Promise<void | FormikErrors<SalePoint>>) {
        const salePoint = salePoints.find(sp => sp.location.name === pickedLocation.name)

        if(!salePoint){
            return
        }

        setValues(salePoint)
    }

    return <Formik
        initialValues={init}
        onSubmit={(values) => onSubmit(values)}>
        {({handleSubmit, handleChange, isValid, values, setValues}) => <Form style={{width: '100%'}} onSubmit={handleSubmit} autoComplete='off'>
            <Stack direction={'row'} spacing={3}>
                <Stack direction={'column'} spacing={3}
                       style={{display: 'flex', justifyContent: 'center', alignItems: 'center'}}>
                    <Typography textAlign={'center'} variant={'h4'}>Nowy punkt sprzedaży</Typography>
                    <MyTextInput
                        inputProps={{disabled: true}}
                        placeholder={'Nazwa punktu'}
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
                            <MenuItem key={1} value={'Poniedziałek'}>Poniedziałek</MenuItem>
                            <MenuItem key={2} value={'Wtorek'}>Wtorek</MenuItem>
                            <MenuItem key={3} value={'Środa'}>Środa</MenuItem>
                            <MenuItem key={4} value={'Czwartek'}>Czwartek</MenuItem>
                            <MenuItem key={5} value={'Piątek'}>Piątek</MenuItem>
                            <MenuItem key={6} value={'Sobota'}>Sobota</MenuItem>
                            <MenuItem key={7} value={'Niedziela'}>Niedziela</MenuItem>
                        </Select>
                    </FormControl>
                    <LocalizationProvider dateAdapter={AdapterDateFns} adapterLocale={pl}>
                        <TimeField
                            required
                            label="Początek sprzedaży"
                            name={'openHours.start'}
                            value={time(values)[0].getDate() ? time(values)[0] : time(values)[2]}
                            onChange={(date) => {
                                setValues({
                                    ...values,
                                    openHours: {end: (values.openHours?.end ?? values.cachedOpenHours!.end), start: (date as Date).toLocaleTimeString()}})
                            }}
                        />
                        <TimeField
                            required
                            label="Koniec sprzedaży"
                            name={'openHours.end'}
                            value={time(values)[1].getDate() ? time(values)[1] : time(values)[3]}
                            onChange={(date) => {
                                setValues({
                                    ...values,
                                    openHours: {start: (values.openHours?.start ?? values.cachedOpenHours!.start), end: (date as Date).toLocaleTimeString()}})
                            }}
                        />
                    </LocalizationProvider>
                    {values.isClosed ?
                        <Tooltip title={'UWAGA - po zastosowaniu zmian sklep będzie otwarty w ten dzień'} open={true}>
                            <Button disabled={values.location.latitude.length < 1 || !validateTime(values)}
                                    type={'submit'} onClick={() => handleSubmit} variant={'contained'}>
                                <Typography>Zaktualizuj</Typography>
                            </Button>
                        </Tooltip>
                        :
                        <Button disabled={values.location.latitude.length < 1 || !validateTime(values)}
                                type={'submit'} onClick={() => handleSubmit} variant={'contained'}>
                            <Typography>Zaktualizuj</Typography>
                        </Button>
                    }
                </Stack>
                <LocationPicker locationName={values.location.name} existingSalePoints={salePoints}
                                onChange={(pickedLocation) => handleLocationPick(pickedLocation, setValues)} />
            </Stack>
        </Form>}
    </Formik>;
}