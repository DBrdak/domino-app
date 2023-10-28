import * as yup from "yup";
import {Form, Formik} from "formik";
import {Button, ButtonGroup, FormControl, InputLabel, MenuItem, Select, Stack, Typography} from "@mui/material";
import React, {useState} from "react";
import {SalePoint} from "../../../../../global/models/shop";
import ShopExtendedLocationPicker from "./ShopExtendedLocationPicker";

interface Props {
    salePoints: SalePoint[],
    onSubmit: (salePointToDelete: SalePoint) => void
}

export function MobileShopSalePointRemoveModal({salePoints, onSubmit}: Props) {
    const [salePoint, setSalePoint] = useState<SalePoint | null>(null)
    const [confirmMode, setConfirmMode] = useState(false)

    return (
        <Stack direction={'column'} spacing={4} style={{display: 'flex', textAlign: 'center', justifyContent: 'center'}}>
            <Typography variant={'h4'}>
                Usuń punkt sprzedaży
            </Typography>
            <ShopExtendedLocationPicker existingSalePoints={salePoints}
                                        onChange={(pickedSalePoint) => setSalePoint(pickedSalePoint)} />
            {confirmMode ?
                <ButtonGroup fullWidth style={{display: 'flex', textAlign: 'center', justifyContent: 'center', gap: '5px'}}>
                    <Button variant={'contained'} onClick={() => salePoint && onSubmit(salePoint)} disabled={!salePoint}>
                        Usuń
                    </Button>
                    <Button variant={'outlined'} onClick={() => setConfirmMode(false)}>
                        Zachowaj
                    </Button>
                </ButtonGroup>
                :
                <div style={{width: '100%', display:'flex', justifyContent: 'center', textAlign: 'center'}}>
                    <Button onClick={() => setConfirmMode(true)} disabled={!salePoint} variant={'contained'}
                            style={{width: '65%'}}>
                        Usuń
                    </Button>
                </div>
            }
        </Stack>
    );
}