import {Form, Formik} from "formik";
import {Button, FormControl, InputLabel, MenuItem, Select, Stack, Typography} from "@mui/material";
import React from "react";
import {SalePoint, ShopWorkingDay, StationaryShop} from "../../../../../global/models/shop";
import {WeekDay} from "../../../../../global/models/common";

interface Props {
    shop: StationaryShop,
    onSubmit: (weekDayToDisable: WeekDay) => void
}

export function StationaryShopWeekDayDisableModal({shop, onSubmit}: Props) {
    const init = {weekDay: ''}

    function handleSubmit(weekDay: string) {
        const workingDay = shop.weekSchedule.find(d => d.weekDay.value === weekDay)
        workingDay && onSubmit(workingDay.weekDay)
    }

    return (
        <Formik
            initialValues={init}
            onSubmit={(values) => handleSubmit(values.weekDay)}
            validateOnMount={true}>
            {({handleSubmit, handleChange, isValid, values}) => (
                <Form style={{width: '100%'}} onSubmit={handleSubmit} autoComplete='off'>
                    <Stack direction={'column'} spacing={2}
                           style={{display: 'flex', justifyContent: 'center', alignItems: 'center'}}>
                        <Typography textAlign={'center'} variant={'h4'}>Zablokuj dzień pracy</Typography>
                        <FormControl fullWidth required>
                            <InputLabel>Dzień tygodnia</InputLabel>
                            <Select
                                name={'weekDay'}
                                defaultValue={''}
                                label="Dzień tygodnia"
                                onChange={handleChange}
                            >
                                {shop.weekSchedule.map(d =>
                                    !d.isClosed && <MenuItem key={d.weekDay.value} value={d.weekDay.value} >{d.weekDay.value}</MenuItem>
                                )}
                            </Select>
                        </FormControl>
                        <Button type={'submit'} onClick={() => handleSubmit} variant={'contained'}>
                            <Typography>Zablokuj</Typography>
                        </Button>
                    </Stack>
                </Form>
            )}
        </Formik>
    );
}