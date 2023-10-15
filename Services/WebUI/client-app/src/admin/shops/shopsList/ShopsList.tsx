import {Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Typography} from "@mui/material";
import LoadingTableRow from "../../../components/LoadingTableRow";
import React from "react";
import {Shop} from "../../../global/models/shop";
import StationaryShopListItem from "./listItems/StationaryShopListItem";
import MobileShopListItem from "./listItems/MobileShopListItem";

interface Props {
    shops: Shop[]
    loading: boolean
}

export function ShopsList({shops, loading}: Props) {

    return (
        <TableContainer>
            <Table>
                <TableHead>
                    <TableRow>
                        <TableCell width={'25%'} style={{textAlign: 'center'}} >
                            <Typography variant='h5'><strong>Nazwa Sklepu</strong></Typography>
                        </TableCell>
                        <TableCell width={'75%'} style={{textAlign: 'center'}}>
                            <Typography variant='h5'><strong>Akcje</strong></Typography>
                        </TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {
                        loading ?
                            <LoadingTableRow rows={4} cells={3} />
                            :
                            shops &&
                            shops.map((p:any) => (
                                p.location ? <StationaryShopListItem shop={p} /> : <MobileShopListItem shop={p} />
                            ))
                    }
                </TableBody>
            </Table>
        </TableContainer>
    )
}